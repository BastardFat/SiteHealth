import { cloneDeep } from 'lodash';

// Usage:
// interpolateWith(foo)`some ${interpolated} string with ${bar}`
//     similar to
// 'some ' + foo(interpolated) + ' string with ' + foo(bar)

function interpolateWith(interpolator) {
    return (strings, ...values) => {
        var result = strings[0];
        for (var i = 0; i < values.length; i++)
            result = result + interpolator(values[i]) + strings[i + 1];
        return result;
    }
}


// Usage:
// let debouncedFn = debounce(x => console.log('debounced: ' + x), 1000);
// debouncedFn(1);
//  ... after some milliseconds < 1000 ...
// debouncedFn(2);
//  ... after second logs "debounced: 2"

// Source: https://toughcompetent.com/es5-es6-debounce-react-events-on-inputs/

function debounce(a, b, c) {
    var d, e;
    return function () {
        function h() {
            d = null, c || (e = a.apply(f, g))
        }
        var f = this,
            g = arguments;
        return clearTimeout(d),
            d = setTimeout(h, b),
            c && !d && (e = a.apply(f, g)),
            e
    }
}

// Based on https://objectpartners.com/2017/04/24/two-way-data-binding-in-reactjs-part-i/
// Usage:
// const userBinder = bindModel(this, 'user', 'onChangeUser')
//  ...
// <input type="text" {...userBinder('name')} >
function bindModel(context, changeHandlerName) {
    const getByKeypath = (p, o) =>
        p.reduce((xs, x) => (xs && xs[x]) ? xs[x] : null, o);

    const createKeys = (p, o, state) =>
        p.reduce((xs, x, i) => {
            if (xs[x] == null) {
                xs[x] =
                    Number.isInteger(p[i + 1]) ?
                        [...(getByKeypath(p.slice(0, i + 1), state) || [])] :
                        {};

            }
            return xs[x];
        }, o);

    return function (...keys) {
        return {
            value: getByKeypath(keys, context.state) || '',
            checked: getByKeypath(keys, context.state) || '',

            onChange(event, data) {
                let value = data.type === 'checkbox' ? data.checked : data.value;

                var newState = cloneDeep(context.state);

                createKeys(keys, newState, context.state);

                getByKeypath(keys.slice(0, -1), newState)[keys.slice(-1)] = value;

                context.setState(newState);

                if (changeHandlerName && typeof context[changeHandlerName] === 'function') {
                    context[changeHandlerName](data, keys);
                }
            }
        };
    }
}


export { interpolateWith, debounce, bindModel }
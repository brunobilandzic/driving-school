export function getKey(...keys): string {
    let key = '';

    keys.forEach((_key, i) => {
        if(i != 0) key += '-'
        key += _key;
    });

    return key;
}
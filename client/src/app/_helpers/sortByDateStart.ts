export function sortByDateDescending(objectArray: any[]) {
    return objectArray.sort(compareDates);
}


function compareDates(obj1: any, obj2: any) {
    return new Date(obj2.dateStart).getTime() - new Date(obj1.dateStart).getTime();
}
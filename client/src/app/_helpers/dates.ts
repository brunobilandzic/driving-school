export function passedToday(dateString: string) {
  return new Date(dateString).getTime() >= Date.now();
}

export function getDateOnly(dateString: string) {
  if (dateString == '') return 'nodate';
  let date = new Date(dateString);
  return (
    padDate(date.getMonth() + 1) +
    '-' +
    padDate(date.getDate()) +
    '-' +
    date.getFullYear()
  );
}

export function padDate(number: number) {
  if (number < 10) return '0' + number.toString();
  else return number.toString();
}

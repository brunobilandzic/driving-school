export function passedToday(dateString: string) {
    return new Date(dateString).getTime() >= Date.now();
  }
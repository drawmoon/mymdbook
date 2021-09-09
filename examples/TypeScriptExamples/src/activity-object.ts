export class ActivityObject<T> {
  private list: T[] = [];

  public wrap(subject: T, callback: () => void) {
    this.add(subject);
    try {
      callback();
    } finally {
      this.remove(subject);
    }
  }

  public count(options?: (objects: T[]) => T[]): number {
    let list = [...this.list];
    if (options) {
      list = options(list);
    }
    return list.length;
  }

  public add(subject: T): void {
    this.list.push(subject);
  }

  public remove(subject: T): void {
    this.list = this.list.filter((p) => p !== subject);
  }
}

import { v4 } from 'uuid';

const activityObjects = new ActivityObject<string>();

console.log(activityObjects.count());
activityObjects.wrap(v4(), () => {
  console.log(activityObjects.count());
});
console.log(activityObjects.count());

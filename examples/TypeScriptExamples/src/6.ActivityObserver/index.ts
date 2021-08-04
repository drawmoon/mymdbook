import { v4 } from 'uuid';
import { sleep } from 'sleep';

class ActivityObserver<T> {
  private activeObject: T[] = [];

  public wrap(subject: T, callback: () => void) {
    this.add(subject);
    try {
      callback();
    } finally {
      this.remove(subject);
    }
  }

  public count(options?: (objects: T[]) => T[]): number {
    let list = [...this.activeObject];
    if (options) {
      list = options(list);
    }
    return list.length;
  }

  public add(subject: T): void {
    this.activeObject.push(subject);
  }

  public remove(subject: T): void {
    this.activeObject = this.activeObject.filter((p) => p !== subject);
  }
}

const observer = new ActivityObserver<string>();

interface Chart {
  id: string;
  dirty: boolean;
}

function getChart(): Chart {
  const chart = {
    id: v4(),
    dirty: true,
  };
  if (chart.dirty) {
    console.log('Refresh chart.');
    observer.wrap(chart.id, () => {
      sleep(5);
      chart.dirty = false;
    });
  } else {
    console.log('Read cache.');
  }
  console.log('Get chart successful.');
  return chart;
}

const chart = getChart();
console.log(chart);

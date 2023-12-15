export interface MessageEvent {
  id: string;
  data: string;
  type: 'event' | 'debug' | 'info';
  retry?: number;
}

export class EventBuilder<T> {
  private id: string;
  private data: string;
  private type: 'event' | 'debug' | 'info' = 'event';
  private retry?: number | undefined;

  public setId(id: string): EventBuilder<T> {
    const builder = this.clone();
    builder.id = id;
    return builder;
  }

  public setData(data: T): EventBuilder<T> {
    const builder = this.clone();
    if (typeof data !== 'string') {
      builder.data = JSON.stringify(data);
    } else {
      builder.data = data;
    }
    return builder;
  }

  public setType(type: 'event' | 'debug' | 'info'): EventBuilder<T> {
    const builder = this.clone();
    builder.type = type;
    return builder;
  }

  public setRetry(retry: number): EventBuilder<T> {
    const builder = this.clone();
    builder.retry = retry;
    return builder;
  }

  public build(): MessageEvent {
    return {
      id: this.id,
      data: this.data,
      type: this.type,
      retry: this.retry,
    };
  }

  private clone(): EventBuilder<T> {
    const builder = new EventBuilder<T>();
    builder.id = this.id;
    builder.data = this.data;
    builder.type = this.type;
    builder.retry = this.retry;
    return builder;
  }
}

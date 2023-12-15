import { Request, Response } from 'express';
import { EventBuilder, MessageEvent } from './message-event';

export class ClientNotifier {
  private readonly messageHub = new MessageHub();

  connect(req: Request, res: Response) {
    // SSE Setup
    res.writeHead(200, {
      'Content-Type': 'text/event-stream',
      'Cache-Control': 'no-cache',
      Connection: 'keep-alive',
    });

    res.write('\n');

    const clientId = req.cookies['user'];
    console.log('establish connection, clientId:', clientId);
    this.messageHub.add({
      id: clientId,
      res: res,
    });

    req.on('close', () => {
      console.log('Close the connection');
      this.messageHub.remove(clientId);
    });
  }

  notify<T>(builder: EventBuilder<T>, req: Request) {
    const clientId = req.cookies['user'];
    const message = builder.setId(clientId).build();
    console.log('Message,', `clientId: ${clientId},`, message);
    this.messageHub.receiveMessage(message);
  }
}

class MessageHub {
  private clients: { id: string; res: Response }[] = [];

  public add(client: { id: string; res: Response }) {
    this.clients.push(client);
  }

  public remove(id: string) {
    this.clients = this.clients.filter((c) => c.id !== id);
  }

  public receiveMessage(message: MessageEvent) {
    const client = this.clients.find((c) => c.id === message.id);
    client.res.write(`id: ${message.id}\n`);
    client.res.write(`data: ${message.data}\n\n`);
  }
}

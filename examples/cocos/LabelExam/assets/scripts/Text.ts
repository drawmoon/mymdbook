import { _decorator, Component, Label, Node } from 'cc';
const { ccclass, property } = _decorator;

@ccclass('Text')
export class Text extends Component {
    start() {
        console.log(this);
        console.log(this.node);

        const labelComp = this.node.getComponent(Label);
        labelComp.string = 'Hello, Cocos!';
    }

    update(deltaTime: number) {
        
    }
}



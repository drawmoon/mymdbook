import { v4 } from 'uuid';
import { ActivityObject } from './activity-object';

const activityObjects = new ActivityObject<string>();

console.log(activityObjects.count());
activityObjects.wrap(v4(), () => {
  console.log(activityObjects.count());
});
console.log(activityObjects.count());

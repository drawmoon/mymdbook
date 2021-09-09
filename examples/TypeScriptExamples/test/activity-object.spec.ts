import { ActivityObject } from '../src/activity-object';

test('TestGetCount', () => {
  const activityObject = new ActivityObject<{ id: string; name: string }>();
  for (let i = 0; i < 5; i++) {
    activityObject.add({
      id: String(i),
      name: 'KKK',
    });
  }
  const all = activityObject.count();
  expect(all).toBe(5);

  const filter = activityObject.count((list) => {
    return list.filter((p) => p.id === '1');
  });
  expect(filter).toBe(1);

  const all2 = activityObject.count();
  expect(all2).toBe(5);
});

test('TestWrap', async () => {
  const activityObject = new ActivityObject<string>();
  await activityObject.wrap('1', async () => {
    const count = activityObject.count();
    expect(count).toBe(1);
  });
  const count = activityObject.count();
  expect(count).toBe(0);
});

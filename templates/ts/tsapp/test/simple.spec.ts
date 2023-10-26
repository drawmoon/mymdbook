import { version } from '../src';

test('version', () => {
  const ver = version().string();

  expect(ver).toBe('1.0.0');
});

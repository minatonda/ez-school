import { spy, assert } from 'sinon';
import { expect } from 'chai';

let loggerSpy = spy();

describe('Test of tests', () => {
  it('1 should be 1', async () => {
    expect(1).to.equal(1);
  });
});

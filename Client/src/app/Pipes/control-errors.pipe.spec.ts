import { ControlErrorsPipe } from './control-errors.pipe';
import { ValidationErrors } from "@angular/forms";

describe('ControlErrorsPipe', () => {
  it('create an instance', () => {
    const pipe = new ControlErrorsPipe();
    expect(pipe).toBeTruthy();
  });

  it('Should return required error message', () => {
    const pipe = new ControlErrorsPipe();
    const errors: ValidationErrors = {'required': true};
    const actual = pipe.transform(errors);
    const expected = 'Field is required.'
    expect(actual).toBe(expected);
  })

  it('Should return email error message', () => {
    const pipe = new ControlErrorsPipe();
    const errors: ValidationErrors = {'email': true};
    const actual = pipe.transform(errors);
    const expected = 'Not valid email.'
    expect(actual).toBe(expected);
  })

  it('Should return minlength error message', () => {
    const pipe = new ControlErrorsPipe();
    const errors: ValidationErrors = {'minlength': {
      'requiredLength': 3
      }};
    const actual = pipe.transform(errors);
    const expected = 'Min length is 3.'
    expect(actual).toBe(expected);
  })
});

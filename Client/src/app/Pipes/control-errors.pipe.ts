import { Pipe, PipeTransform } from '@angular/core';
import { ValidationErrors } from '@angular/forms';

@Pipe({
  name: 'controlErrors',
  standalone: true,
  pure: false,
})

export class ControlErrorsPipe implements PipeTransform {
  private get validationErrorsMessages(): { [key: string]: string | null } {
    return {
      'required': 'Field is required.',
      'email': 'Not valid email.',
      'minlength': 'Min length is {{requiredLength}}.'
    }
  };

  transform(value: ValidationErrors | null): string | null {
    if (!value) return null;

    return Object.keys(value).map(errorType => {
      const message = this.validationErrorsMessages[errorType];
      if (!message) return null;
      const attributes = Object.getOwnPropertyNames(value[errorType])
      const currentError = Object(value[errorType]);
      return attributes.reduce((previousValue, currentValue) => previousValue.replaceAll(`{{${currentValue}}}`, currentError[currentValue]), message);
    }).filter(value => value)
      .join('<br>');
  }
}

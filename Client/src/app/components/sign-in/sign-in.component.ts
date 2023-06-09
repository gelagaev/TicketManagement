import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatGridListModule } from '@angular/material/grid-list';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ControlErrorsPipe } from '../../pipes/control-errors.pipe';
import { Store } from '@ngrx/store';
import { AuthActions, NavigationActions } from '../../store/actions';

@Component({
  selector: 'tm-sign-in',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatFormFieldModule, MatInputModule, MatGridListModule, ReactiveFormsModule, ControlErrorsPipe],
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})

export class SignInComponent {
  constructor(private store: Store) {
  }

  form = new FormGroup({
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    password: new FormControl<string>('', [Validators.required, Validators.minLength(6)])
  })

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.store.dispatch(AuthActions.signIn({
        email: this.form.controls.email.value!,
        password: this.form.controls.password.value!
      })
    );
  }

  onRegister(): void {
    this.store.dispatch(NavigationActions.navigateToRegister());
  }
}

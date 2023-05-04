import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ControlErrorsPipe } from "../../pipes/control-errors.pipe";
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatGridListModule } from "@angular/material/grid-list";
import { MatInputModule } from "@angular/material/input";
import { Store } from "@ngrx/store";
import { AuthActions, NavigationActions } from "../../store/actions";

@Component({
  selector: 'tm-register',
  standalone: true,
  imports: [CommonModule, ControlErrorsPipe, FormsModule, MatButtonModule, MatCardModule, MatFormFieldModule, MatGridListModule, MatInputModule, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegisterComponent {
  constructor(private store: Store) {
  }

  form = new FormGroup({
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    password: new FormControl<string>('', [Validators.required, Validators.minLength(6)]),
    firstName: new FormControl<string>('', [Validators.required]),
    lastName: new FormControl<string>('', [Validators.required])
  })

  onSubmit(): void {
    if (this.form.invalid) return;

    this.store.dispatch(AuthActions.register({
      email: this.form.controls.email.value!,
      firstName: this.form.controls.firstName.value!,
      lastName: this.form.controls.lastName.value!,
      password: this.form.controls.password.value!
    }))
  }

  onSignIn(): void {
    this.store.dispatch(NavigationActions.navigateToSignIn())
  }
}

import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from "@angular/material/card";
import { MatButtonModule } from "@angular/material/button";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatGridListModule } from "@angular/material/grid-list";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { ControlErrorsPipe } from "../../Pipes/errors-pipe.pipe";

@Component({
  selector: 'tm-sign-in',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatFormFieldModule, MatInputModule, MatGridListModule, ReactiveFormsModule, ControlErrorsPipe],
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.less']
})

export class SignInComponent {
  form = new FormGroup({
    email: new FormControl<string>('', [Validators.required, Validators.email]),
    password: new FormControl<string>('', [Validators.required, Validators.minLength(6)])
  })

  public onSubmit() {
    console.log("");
  }
}

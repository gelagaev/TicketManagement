import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ControlErrorsPipe } from "../../pipes/control-errors.pipe";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatGridListModule } from "@angular/material/grid-list";
import { MatInputModule } from "@angular/material/input";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { MatExpansionModule } from "@angular/material/expansion";
import { ICreateTicketRequest } from "../../services/web-api-service-proxies";
import { Observable, tap } from "rxjs";
import { UntilDestroy, untilDestroyed } from "@ngneat/until-destroy";

@UntilDestroy()
@Component({
  selector: 'tm-create-ticket',
  standalone: true,
  imports: [CommonModule, ControlErrorsPipe, MatButtonModule, MatCardModule, MatFormFieldModule, MatGridListModule, MatInputModule, ReactiveFormsModule, MatExpansionModule],
  templateUrl: './create-ticket.component.html',
  styleUrls: ['./create-ticket.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CreateTicketComponent implements OnInit {
  @Input({required: true})
  createTicketSuccess$!: Observable<any>;

  @Output()
  submit = new EventEmitter<ICreateTicketRequest>();

  ngOnInit(): void {
    this.createTicketSuccess$.pipe(
      untilDestroyed(this),
      tap(() => this.form.reset()))
      .subscribe();
  }

  form = new FormGroup({
    subject: new FormControl<string>('', [Validators.required, Validators.maxLength(100)]),
    description: new FormControl<string>('', [Validators.required, Validators.maxLength(1000)])
  })

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.submit.emit({
      subject: this.form.controls.subject.value!,
      description: this.form.controls.description.value!,
    });
  }
}

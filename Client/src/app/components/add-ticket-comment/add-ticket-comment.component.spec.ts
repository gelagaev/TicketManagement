import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AddTicketCommentComponent } from './add-ticket-comment.component';

describe('AddTicketCommentComponent', () => {
  let component: AddTicketCommentComponent;
  let fixture: ComponentFixture<AddTicketCommentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ AddTicketCommentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddTicketCommentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

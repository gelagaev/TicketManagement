import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketCommentListComponent } from './ticket-comment-list.component';

describe('TicketCommentListComponent', () => {
  let component: TicketCommentListComponent;
  let fixture: ComponentFixture<TicketCommentListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ TicketCommentListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TicketCommentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

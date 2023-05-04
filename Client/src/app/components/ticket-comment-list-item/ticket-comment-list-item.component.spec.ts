import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketCommentListItemComponent } from './ticket-comment-list-item.component';

describe('TicketCommentListItemComponent', () => {
  let component: TicketCommentListItemComponent;
  let fixture: ComponentFixture<TicketCommentListItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ TicketCommentListItemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TicketCommentListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

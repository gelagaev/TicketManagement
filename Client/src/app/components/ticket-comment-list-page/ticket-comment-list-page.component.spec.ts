import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketCommentListPageComponent } from './ticket-comment-list-page.component';

describe('TicketCommentListPageComponent', () => {
  let component: TicketCommentListPageComponent;
  let fixture: ComponentFixture<TicketCommentListPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [TicketCommentListPageComponent]
    });
    fixture = TestBed.createComponent(TicketCommentListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

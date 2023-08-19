import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatListarComponent } from './chat-listar.component';

describe('ChatListarComponent', () => {
  let component: ChatListarComponent;
  let fixture: ComponentFixture<ChatListarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChatListarComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatListarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

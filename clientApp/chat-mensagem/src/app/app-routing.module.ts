import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChatListarComponent } from './features/chat';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/chat-listar',
    pathMatch: 'full'
  },
  {
    path: "chat-listar",
    component: ChatListarComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

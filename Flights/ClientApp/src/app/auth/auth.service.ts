import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor() {}

  currentUser?: User;

  loginUser(user: User) {
    console.log(user);

    this.currentUser = user;
  }
}

interface User {
  email?: string | null;
}

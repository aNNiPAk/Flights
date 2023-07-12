import { Component } from '@angular/core';
import { PassengerService } from '../api/services';
import { FormBuilder } from '@angular/forms';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-passenger',
  templateUrl: './register-passenger.component.html',
  styleUrls: ['./register-passenger.component.css'],
})
export class RegisterPassengerComponent {
  constructor(
    private passengerService: PassengerService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  form = this.fb.group({
    email: [''],
    firstName: [''],
    lastName: [''],
    isFemale: [true],
  });

  checkPassenger(): void {
    const params = { email: this.form.value.email ?? '' };

    if (params.email !== '') {
      this.passengerService.findPassenger(params).subscribe({
        next: this.login,
        error: (err) => {
          if (err.status !== 404) console.error(err);
        },
      });
    }
  }

  register() {
    console.log(this.form.value);

    this.passengerService
      .registerPassenger({ body: this.form.value })
      .subscribe(this.login, console.log);
  }

  private login = () => {
    this.authService.loginUser({ email: this.form.value.email });

    this.router.navigate(['/search-flight']);
  };
}

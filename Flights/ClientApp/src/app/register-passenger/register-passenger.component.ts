import { Component } from '@angular/core';
import { PassengerService } from '../api/services';
import { FormBuilder, Validators } from '@angular/forms';
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
    email: [
      '',
      Validators.compose([
        Validators.required,
        Validators.email,
        Validators.minLength(3),
        Validators.maxLength(100),
      ]),
    ],
    firstName: [
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(35),
      ]),
    ],
    lastName: [
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(35),
      ]),
    ],
    isFemale: [true, Validators.required],
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
    if (this.form.invalid) return;

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

import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FlightService } from '../api/services/flight.service';
import { BookDto, FlightRm } from '../api/models';
import { AuthService } from '../auth/auth.service';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-book-flight',
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css'],
})
export class BookFlightComponent {
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private flightService: FlightService,
    private authService: AuthService,
    private fb: FormBuilder
  ) {}

  flightId: string = 'not loaded';
  flight: FlightRm = {};

  form = this.fb.group({
    number: [
      1,
      Validators.compose([
        Validators.required,
        Validators.min(1),
        Validators.max(254),
      ]),
    ],
  });

  get number() {
    return this.form.controls.number;
  }

  ngOnInit() {
    this.route.paramMap.subscribe((p) => this.findFlight(p.get('flightId')));
  }

  private findFlight = (flightId: string | null) => {
    this.flightId = flightId ?? 'not loaded';

    this.flightService
      .findFlight({ id: this.flightId })
      .subscribe((flight) => (this.flight = flight), this.errorHandling);
  };

  private errorHandling = (err: any) => {
    if (err.status == 404) {
      alert('Flight not found!');
      this.router.navigate(['/search-flight']);
    }

    if (err.status == 409) {
      console.log('err: ', err);
      alert(JSON.parse(err.error).message);
    }

    console.log('Response error. Status: ', err.status);
    console.log('Response error. Status text: ', err.statusText);
  };

  public book(): void {
    if (this.form.invalid) return;

    console.log(this.form.value.number, this.flightId);

    const booking: BookDto = {
      flightId: this.flight.id,
      passengerEmail: this.authService.currentUser?.email,
      numberOfSeats: this.form.value?.number ?? 0,
    };

    this.flightService.bookFlight({ body: booking }).subscribe({
      next: (_) => this.router.navigate(['/my-bookings']),
      error: this.errorHandling,
    });
  }
}

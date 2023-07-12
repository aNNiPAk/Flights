import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FlightService } from '../api/services/flight.service';
import { FlightRm } from '../api/models';
import { AuthService } from '../auth/auth.service';
import { FormBuilder } from '@angular/forms';

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
    number: [1],
  });

  ngOnInit() {
    if (!this.authService.currentUser) {
      this.router.navigate(['/register-passenger']);
    }

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

    console.log(err);
  };

  book() {
    console.log(this.form.value.number, this.flightId);
  }
}

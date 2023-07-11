import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FlightService } from '../api/services/flight.service';
import { FlightRm } from '../api/models';

@Component({
  selector: 'app-book-flight',
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css'],
})
export class BookFlightComponent {
  flightId: string = 'not loaded';
  flight: FlightRm = {};

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private flightService: FlightService
  ) {}

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

    console.log(err);
  };
}

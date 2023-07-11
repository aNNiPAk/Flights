import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-book-flight',
  templateUrl: './book-flight.component.html',
  styleUrls: ['./book-flight.component.css'],
})
export class BookFlightComponent {
  flightId: string = 'not loaded';

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.paramMap.subscribe(
      (p) => (this.flightId = this.findFlight(p.get('flightId')))
    );
  }

  private findFlight = (flightId: string | null) =>
    (this.flightId = flightId ?? 'not loaded');
}

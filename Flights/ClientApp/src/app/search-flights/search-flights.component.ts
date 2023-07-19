import { Component } from '@angular/core';
import { FlightService } from '../api/services/flight.service';
import { FlightRm } from '../api/models';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css'],
})
export class SearchFlightsComponent {
  constructor(private flightService: FlightService, private fb: FormBuilder) {}

  searchForm = this.fb.group({
    from: [''],
    destination: [''],
    fromDate: [''],
    toDate: [''],
    numberOfPassenger: [1],
  });

  searchResult: FlightRm[] = [];

  search(): void {
    const searchParams = {
      from: this.searchForm.get('from')?.value || '',
      destination: this.searchForm.get('destination')?.value || '',
      fromDate: this.searchForm.get('fromDate')?.value || '',
      toDate: this.searchForm.get('toDate')?.value || '',
      numberOfPassenger: this.searchForm.get('numberOfPassenger')?.value || 1,
    };

    this.flightService.searchFlight(searchParams).subscribe({
      next: (r) => (this.searchResult = r),
      error: this.handleError,
    });
  }

  private handleError(err: any): void {
    console.log(err);
  }
}

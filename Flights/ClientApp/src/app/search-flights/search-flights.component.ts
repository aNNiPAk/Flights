import { Component } from '@angular/core';

@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})
export class SearchFlightsComponent {
  searchResult: IFlightRm[] = [
    {
      airline: 'American Airlines',
      remainingNumberOfSeats: 500,
      departure: { time: Date.now().toString(), place: 'Los Angeles' },
      arrival: { time: Date.now().toString(), place: 'Istanbul' },
      price: '350'
    },
    {
      airline: 'Deutsche BA',
      remainingNumberOfSeats: 60,
      departure: { time: Date.now().toString(), place: 'Munchen' },
      arrival: { time: Date.now().toString(), place: 'Schiphol' },
      price: '600'
    },
    {
      airline: 'British Airways',
      remainingNumberOfSeats: 60,
      departure: { time: Date.now().toString(), place: 'London, England' },
      arrival: { time: Date.now().toString(), place: 'Vizzola-Ticino' },
      price: '600'
    }
  ]
}

export interface IFlightRm {
  airline: string;
  arrival: ITimePalceRm;
  departure: ITimePalceRm;
  price: string;
  remainingNumberOfSeats: number;
}

export interface ITimePalceRm {
  place: string;
  time: string;
}

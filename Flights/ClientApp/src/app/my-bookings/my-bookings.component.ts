import { Component, OnInit } from '@angular/core';
import { BookingRm } from '../api/models/booking-rm';
import { BookingService } from '../api/services/booking.service';
import { AuthService } from '../auth/auth.service';
import { BookDto } from '../api/models';

@Component({
  selector: 'app-my-bookings',
  templateUrl: './my-bookings.component.html',
  styleUrls: ['./my-bookings.component.css'],
})
export class MyBookingsComponent implements OnInit {
  bookings!: BookingRm[];

  constructor(
    private bookingsService: BookingService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.bookingsService
      .listBooking({ email: this.authService.currentUser?.email ?? '' })
      .subscribe((r) => (this.bookings = r), this.handleError);
  }

  private handleError(err: any) {
    console.log('Response Error, Status: ' + err.status);
    console.log('Response Error, Status text: ' + err.statusText);
    console.log(err);
  }

  cancel(booking: BookingRm) {
    const dto: BookDto = {
      flightId: booking.flightId,
      numberOfSeats: booking.numberOfBookedSeats,
      passengerEmail: booking.passengerEmail,
    };

    this.bookingsService.cancelBooking({ body: dto }).subscribe({
      next: (_) => (this.bookings = this.bookings.filter((b) => b != booking)),
      error: this.handleError,
    });
  }
}

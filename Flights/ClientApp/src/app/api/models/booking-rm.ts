/* tslint:disable */
/* eslint-disable */
import { TimePlaceRm } from './time-place-rm';
export interface BookingRm {
  airline?: null | string;
  arrival?: TimePlaceRm;
  departure?: TimePlaceRm;
  flightId?: string;
  passengerEmail?: null | string;
  price?: null | string;
  remainingNumberOfSeats?: number;
}

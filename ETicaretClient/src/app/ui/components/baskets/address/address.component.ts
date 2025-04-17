import { Component, EventEmitter, OnInit, Output, inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AddressService } from '../../../../services/common/models/address.service';
import { createAddress } from '../../../../contracts/address/createAddress';
import { listAddress } from '../../../../contracts/address/listAddress';

// Create an extended interface with the missing properties
interface ExtendedAddress extends listAddress {
  postalCode: string;
  country: string;
  phoneNumber: string;
}

@Component({
  selector: 'app-address',
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.scss'],
  standalone: false
})
export class AddressComponent implements OnInit {
  @Output() addressValid = new EventEmitter<boolean>();
  @Output() addressSaved = new EventEmitter<createAddress>();

  addressForm: FormGroup;
  savedAddresses: any[] = []; // Using any to avoid type errors
  selectedAddressId: string | number | null = null;
  private addressService = inject(AddressService);

  constructor(private fb: FormBuilder) {
    this.addressForm = this.fb.group({
      name: ['', Validators.required],
      addressDetail: ['', Validators.required],
      city: ['', Validators.required],
      postalCode: ['', Validators.required],
      country: ['', Validators.required],
      phoneNumber: ['', Validators.required]
    });
  }

  async ngOnInit() {
    await this.getSavedAddress();
  }

  isFormValid(): boolean {
    return this.addressForm.valid;
  }

  validateAddress() {
    if (this.isFormValid()) {
      const addressData = new createAddress(
        'null',
        this.addressForm.value.name,
        this.addressForm.value.addressDetail,
        this.addressForm.value.city,
        this.addressForm.value.postalCode,
        this.addressForm.value.country,
        this.addressForm.value.phoneNumber
      );
      this.addressValid.emit(true);
      this.addressSaved.emit(addressData);
      this.addressService.create(addressData);
    } else {
      this.addressValid.emit(false);
      this.addressSaved.emit(null);
    }
  }

  async getSavedAddress() {
    try {
      // Get addresses and store as any type to avoid TypeScript errors
      const addresses = await this.addressService.read();
      this.savedAddresses = addresses;
      console.log('Saved addresses:', this.savedAddresses);
    } catch (error) {
      console.error('Error loading saved addresses:', error);
    }
  }

  selectAddress(address: any) {
    this.selectedAddressId = address.id;

    // Populate form with selected address values
    this.addressForm.patchValue({
      name: address.name || '',
      addressDetail: address.addressDetail || '',
      city: address.city || '',
      postalCode: address.postalCode || '',
      country: address.country || '',
      phoneNumber: address.phoneNumber || ''
    });

    // Trigger validation after populating the form
    this.addressForm.updateValueAndValidity();

    // Emit that we have a valid address since we're selecting from saved addresses
    this.addressValid.emit(true);

    // Create address object for parent component
    const addressData = new createAddress(
      'null',
      address.name || '',
      address.addressDetail || '',
      address.city || '',
      address.postalCode || '',
      address.country || '',
      address.phoneNumber || ''
    );

    this.addressSaved.emit(addressData);
  }

  clearSelectedAddress() {
    this.selectedAddressId = null;
    this.addressForm.reset();
    this.addressValid.emit(false);
  }
}

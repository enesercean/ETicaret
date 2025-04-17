import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { listAddress } from '../../../../contracts/address/listAddress';
import { AddressService } from '../../../../services/common/models/address.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../../services/ui/custom-toastr.service';
import { SpinnerService } from '../../../../services/common/spinner/spinner.service';
import { createAddress } from '../../../../contracts/address/createAddress';

@Component({
  selector: 'app-address-book',
  standalone: false,
  templateUrl: './address-book.component.html',
  styleUrl: './address-book.component.scss'
})
export class AddressBookComponent implements OnInit {
  addresses: listAddress[] = [];
  displayAddressDialog: boolean = false;
  addressForm: FormGroup;
  dialogHeader: string = 'Add New Address';
  editingAddressId: string | null = null;

  private addressService = inject(AddressService);
  private toastrService = inject(CustomToastrService);
  private spinnerService = inject(SpinnerService);
  private formBuilder = inject(FormBuilder);

  constructor(private router: Router) {
    this.addressForm = this.formBuilder.group({
      name: ['', Validators.required],
      addressDetail: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      postalCode: ['', Validators.required],
      phoneNumber: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadAddresses();
  }

  async loadAddresses() {
    try {
      setTimeout(() => this.spinnerService.showSpinner(), 0);
      this.addresses = await this.addressService.read();

      if (this.addresses.length > 0) {
        console.log('API response first address:', this.addresses[0]);
      }
    } catch (error) {
      console.error('Error loading addresses:', error);
    } finally {
      setTimeout(() => this.spinnerService.hideSpinner(), 0);
    }
  }

  openAddressForm(): void {
    this.showAddDialog();
  }

  showAddDialog(): void {
    this.dialogHeader = 'Add New Address';
    this.editingAddressId = null;
    this.addressForm.reset();
    this.displayAddressDialog = true;
  }

  hideDialog(): void {
    this.displayAddressDialog = false;
    this.addressForm.reset();
  }

  editAddress(addressId: string): void {
    this.editingAddressId = addressId;
    this.dialogHeader = 'Edit Address';

    // Find address by converting id to string for comparison
    const addressToEdit = this.addresses.find(addr => String(addr.id) === addressId);
    if (addressToEdit) {
      this.addressForm.patchValue({
        name: addressToEdit.name,
        addressDetail: addressToEdit.addressDetail,
        city: addressToEdit.city,
        country: addressToEdit.country,
        postalCode: addressToEdit.postalCode,
        phoneNumber: addressToEdit.phoneNumber
      });
      this.displayAddressDialog = true;
    }
  }

  async saveAddress(): Promise<void> {
    if (this.addressForm.invalid) {
      return;
    }

    try {
      setTimeout(() => this.spinnerService.showSpinner(), 0);

      const addressData: any = {
        ...this.addressForm.value
      };

      if (this.editingAddressId) {
        // Güncelleme yaparken id bilgisini nesneye ekliyoruz
        addressData.id = this.editingAddressId;

        console.log('Updating address with data:', addressData);
        await this.addressService.update(addressData);

        this.toastrService.message('Address updated successfully', 'Success', {
          messageType: ToastrMessageType.Success,
          position: ToastrPosition.TopRight
        });
      } else {
        await this.addressService.create(addressData);
        this.toastrService.message('Address added successfully', 'Success', {
          messageType: ToastrMessageType.Success,
          position: ToastrPosition.TopRight
        });
      }

      await this.loadAddresses();
      this.hideDialog();

    } catch (error) {
      console.error('Error saving address:', error);
      this.toastrService.message('An error occurred while saving address', 'Error', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    } finally {
      setTimeout(() => this.spinnerService.hideSpinner(), 0);
    }
  }


  async deleteAddress(addressId: string): Promise<void> {
    if (confirm('Bu adresi silmek istediğinizden emin misiniz?')) {
      try {
        setTimeout(() => this.spinnerService.showSpinner(), 0);
        await this.addressService.delete(addressId);
        this.addresses = this.addresses.filter(address => String(address.id) !== addressId);
        this.toastrService.message('Address deleted successfully', 'Success', {
          messageType: ToastrMessageType.Success,
          position: ToastrPosition.TopRight
        });
      } catch (error) {
        this.toastrService.message('An error occurred while deleting the address', 'Error', {
          messageType: ToastrMessageType.Error,
          position: ToastrPosition.TopRight
        });
      } finally {
        setTimeout(() => this.spinnerService.hideSpinner(), 0);
      }
    }
  }

  async setAsDefault(addressId: string): Promise<void> {
    try {
      setTimeout(() => this.spinnerService.showSpinner(), 0);
      // TODO: Replace with actual API call to set default address
      console.log(`Address ${addressId} set as default`);

      this.toastrService.message('Address set as default', 'Success', {
        messageType: ToastrMessageType.Success,
        position: ToastrPosition.TopRight
      });
    } catch (error) {
      this.toastrService.message('An error occurred while setting default address', 'Error', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    } finally {
      setTimeout(() => this.spinnerService.hideSpinner(), 0);
    }
  }
}

import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ConfirmationService } from 'primeng/api';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../../services/ui/custom-toastr.service';
import { SpinnerService } from '../../../../services/common/spinner/spinner.service';

// Create interfaces for the payment method
interface PaymentMethod {
  id: number;
  cardName: string;
  cardholderName: string;
  cardNumber: string;
  expiryMonth: string;
  expiryYear: string;
  cvv: string;
  cardType: string;
  isDefault: boolean;
}

interface DropdownOption {
  name: string;
  value: string;
}

@Component({
  selector: 'app-payment-options',
  templateUrl: './payment-options.component.html',
  styleUrls: ['./payment-options.component.scss'],
  standalone: false
})
export class PaymentOptionsComponent implements OnInit {
  paymentMethods: PaymentMethod[] = [];
  displayPaymentDialog: boolean = false;
  paymentForm: FormGroup;
  dialogHeader: string = 'Add New Payment Method';
  editingPaymentId: string | null = null;

  // Dropdown options
  months: DropdownOption[] = [];
  years: DropdownOption[] = [];
  cardTypes: DropdownOption[] = [
    { name: 'Visa', value: 'visa' },
    { name: 'Mastercard', value: 'mastercard' },
    { name: 'American Express', value: 'amex' },
    { name: 'Discover', value: 'discover' }
  ];

  private toastrService = inject(CustomToastrService);
  private spinnerService = inject(SpinnerService);
  private formBuilder = inject(FormBuilder);
  private confirmationService = inject(ConfirmationService);

  constructor(private router: Router) {
    this.paymentForm = this.formBuilder.group({
      cardName: ['', Validators.required],
      cardholderName: ['', Validators.required],
      cardNumber: ['', [Validators.required, Validators.pattern(/^\d{4}\s\d{4}\s\d{4}\s\d{4}$/)]],
      expiryMonth: ['', Validators.required],
      expiryYear: ['', Validators.required],
      cvv: ['', [Validators.required, Validators.pattern(/^\d{3}$/)]],
      cardType: ['', Validators.required]
    });

    // Initialize months dropdown
    for (let i = 1; i <= 12; i++) {
      const month = i.toString().padStart(2, '0');
      this.months.push({ name: month, value: month });
    }

    // Initialize years dropdown (current year + 10 years)
    const currentYear = new Date().getFullYear();
    for (let i = currentYear; i <= currentYear + 10; i++) {
      this.years.push({ name: i.toString(), value: i.toString() });
    }
  }

  ngOnInit(): void {
    this.loadPaymentMethods();
  }

  loadPaymentMethods() {
    try {
      setTimeout(() => this.spinnerService.showSpinner(), 0);
      
      // Simulating API call with mock data
      // Replace this with actual API call when available
      this.paymentMethods = [
        {
          id: 1,
          cardName: 'My Personal Card',
          cardholderName: 'John Doe',
          cardNumber: '4111111111111111',
          expiryMonth: '12',
          expiryYear: '2025',
          cvv: '123',
          cardType: 'visa',
          isDefault: true
        },
        {
          id: 2,
          cardName: 'Work Expenses',
          cardholderName: 'John Doe',
          cardNumber: '5555555555554444',
          expiryMonth: '06',
          expiryYear: '2026',
          cvv: '321',
          cardType: 'mastercard',
          isDefault: false
        }
      ];
      
    } catch (error) {
      console.error('Error loading payment methods:', error);
      this.toastrService.message('Error loading payment methods', 'Error', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    } finally {
      setTimeout(() => this.spinnerService.hideSpinner(), 0);
    }
  }

  showAddDialog(): void {
    this.dialogHeader = 'Add New Payment Method';
    this.editingPaymentId = null;
    this.paymentForm.reset();
    this.displayPaymentDialog = true;
  }

  hideDialog(): void {
    this.displayPaymentDialog = false;
    this.paymentForm.reset();
  }

  editPaymentMethod(paymentId: string): void {
    this.editingPaymentId = paymentId;
    this.dialogHeader = 'Edit Payment Method';

    const paymentToEdit = this.paymentMethods.find(p => String(p.id) === paymentId);
    if (paymentToEdit) {
      // Don't include the actual CVV for security reasons
      this.paymentForm.patchValue({
        cardName: paymentToEdit.cardName,
        cardholderName: paymentToEdit.cardholderName,
        cardNumber: paymentToEdit.cardNumber,
        expiryMonth: paymentToEdit.expiryMonth,
        expiryYear: paymentToEdit.expiryYear,
        cvv: '', // Don't prefill CVV for security
        cardType: paymentToEdit.cardType
      });
      this.displayPaymentDialog = true;
    }
  }

  savePaymentMethod(): void {
    if (this.paymentForm.invalid) {
      return;
    }

    try {
      setTimeout(() => this.spinnerService.showSpinner(), 0);

      const paymentData = {
        ...this.paymentForm.value,
        // Remove spaces from card number for storage
        cardNumber: this.paymentForm.value.cardNumber.replace(/\s/g, '')
      };

      if (this.editingPaymentId) {
        // Update existing payment method
        const index = this.paymentMethods.findIndex(p => String(p.id) === this.editingPaymentId);
        if (index !== -1) {
          this.paymentMethods[index] = {
            ...this.paymentMethods[index],
            ...paymentData,
            id: parseInt(this.editingPaymentId)
          };
        }

        this.toastrService.message('Payment method updated successfully', 'Success', {
          messageType: ToastrMessageType.Success,
          position: ToastrPosition.TopRight
        });
      } else {
        // Add new payment method
        const newId = Math.max(...this.paymentMethods.map(p => p.id), 0) + 1;
        this.paymentMethods.push({
          ...paymentData,
          id: newId,
          isDefault: this.paymentMethods.length === 0 // First card is default
        });

        this.toastrService.message('Payment method added successfully', 'Success', {
          messageType: ToastrMessageType.Success,
          position: ToastrPosition.TopRight
        });
      }

      this.hideDialog();

    } catch (error) {
      console.error('Error saving payment method:', error);
      this.toastrService.message('An error occurred while saving payment method', 'Error', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    } finally {
      setTimeout(() => this.spinnerService.hideSpinner(), 0);
    }
  }

  deletePaymentMethod(paymentId: string): void {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete this payment method?',
      header: 'Delete Confirmation',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        try {
          setTimeout(() => this.spinnerService.showSpinner(), 0);
          this.paymentMethods = this.paymentMethods.filter(p => String(p.id) !== paymentId);
          
          this.toastrService.message('Payment method deleted successfully', 'Success', {
            messageType: ToastrMessageType.Success,
            position: ToastrPosition.TopRight
          });
        } catch (error) {
          this.toastrService.message('An error occurred while deleting the payment method', 'Error', {
            messageType: ToastrMessageType.Error,
            position: ToastrPosition.TopRight
          });
        } finally {
          setTimeout(() => this.spinnerService.hideSpinner(), 0);
        }
      }
    });
  }

  setAsDefault(paymentId: string): void {
    try {
      setTimeout(() => this.spinnerService.showSpinner(), 0);
      
      // Update the default status
      this.paymentMethods.forEach(payment => {
        payment.isDefault = String(payment.id) === paymentId;
      });

      this.toastrService.message('Payment method set as default', 'Success', {
        messageType: ToastrMessageType.Success,
        position: ToastrPosition.TopRight
      });
    } catch (error) {
      this.toastrService.message('An error occurred while setting default payment method', 'Error', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    } finally {
      setTimeout(() => this.spinnerService.hideSpinner(), 0);
    }
  }
}

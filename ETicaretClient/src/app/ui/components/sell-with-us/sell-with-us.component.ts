import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { SupplierService } from '../../../services/common/models/supplier.service';

@Component({
  selector: 'app-sell-with-us',
  standalone: false,
  templateUrl: './sell-with-us.component.html',
  styleUrl: './sell-with-us.component.scss'
})
export class SellWithUsComponent implements OnInit {

  private supplierService = inject(SupplierService);
  // Seller success stories for the carousel
  sellerStories = [
    {
      sellerName: 'Emily Johnson',
      businessType: 'Handcrafted Jewelry',
      quote: 'Since joining this platform, my small jewelry business has transformed into a thriving online store. The seller tools made inventory management so simple, and the customer reach is incredible.',
      growthPercent: 215,
      monthsOnPlatform: 8,
      sellerImage: 'assets/images/sellers/seller-1.jpg'
    },
    {
      sellerName: 'David Lee',
      businessType: 'Home Decor',
      quote: 'I was skeptical at first, but the results speak for themselves. My home decor business went from local craft fairs to nationwide sales in just months. The support team was there every step of the way.',
      growthPercent: 180,
      monthsOnPlatform: 12,
      sellerImage: 'assets/images/sellers/seller-2.jpg'
    },
    {
      sellerName: 'Maria Rodriguez',
      businessType: 'Organic Skincare',
      quote: 'The exposure my organic skincare line has received is beyond what I could have achieved on my own. The platforms marketing features and targeted promotions have helped me build a loyal customer base.',
      growthPercent: 320,
      monthsOnPlatform: 10,
      sellerImage: 'assets/images/sellers/seller-3.jpg'
    },
    {
      sellerName: 'James Wilson',
      businessType: 'Vintage Collectibles',
      quote: 'As a seller of vintage items, I needed a platform that could connect me with the right customers. Not only did I find those customers, but the seamless payment processing and shipping tools saved me countless hours.',
      growthPercent: 150,
      monthsOnPlatform: 18,
      sellerImage: 'assets/images/sellers/seller-4.jpg'
    }
  ];

  constructor(private router: Router) { }

  ngOnInit() {
    // Any initialization logic can go here
  }

  // Method to navigate to seller registration
  navigateToSellerRegistration() {
    this.router.navigate(['/sellwithus/register-seller']);
  }

  // Method to scroll to a specific section
  scrollToSection(sectionId: string) {
    const element = document.getElementById(sectionId);
    if (element) {
      element.scrollIntoView({ behavior: 'smooth' });
    }
  }
}

import { Component, HostListener, Inject, OnInit, inject } from '@angular/core';
import { ProductService } from '../../../../services/common/models/product.service';
import { List_Product } from '../../../../contracts/List_Product';
import { BasketService } from '../../../../services/common/models/basket.service';
import { createBasketItem } from '../../../../contracts/basket/createBasketItem';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../../services/ui/custom-toastr.service';
import { CategoryService } from '../../../../services/common/models/category.service';
import { listCategory } from '../../../../contracts/category/listCategory';
import { listCategoryId } from '../../../../contracts/category/listCategoryId';
import { TreeNode } from 'primeng/api';
import { BrandService } from '../../../../services/common/models/brand.service';
import { ProductLikeService } from '../../../../services/common/models/product-like.service';
import { Router } from '@angular/router';
import { SpinnerService } from '../../../../services/common/spinner/spinner.service';
import { SharedDataService } from '../../../../services/common/shared-data.service';

@Component({
  selector: 'app-list',
  standalone: false,
  templateUrl: './list.component.html',
  styleUrl: './list.component.scss'
})
export class ListComponent implements OnInit {
  constructor(
    @Inject('baseUrl') public baseUrl: string,
    private router: Router,
    private spinnerService: SpinnerService
  ) { }

  customToastrService = inject(CustomToastrService);
  basketService = inject(BasketService);
  productService = inject(ProductService);
  categoryService = inject(CategoryService);
  brandService = inject(BrandService);
  private productLikeService = inject(ProductLikeService);
  private sharedDataService = inject(SharedDataService);

  products: List_Product[] = null;
  categories: TreeNode[] = [];
  displayedProducts: List_Product[] = [];
  itemsPerPage = 24;
  currentPage = 1;
  loading = false;
  value!: number;
  selectedCategories: TreeNode[] = [];
  selectedNodes: TreeNode[] = [];
  listCategoryIds: listCategoryId = new listCategoryId();

  sortOptions = [
    { label: 'Price Asc', value: 'priceAsc' },
    { label: 'Price Desc', value: 'priceDesc' },
    { label: 'Newest', value: 'newest' }
  ];
  selectedSortOption: string = 'newest';

  likedProducts: Set<string> = new Set();

  selectedBrands: any[] = [];
  brandOptions = [];

  minPrice: number;
  maxPrice: number;

  async ngOnInit() {
    this.spinnerService.showSpinner();

    this.sharedDataService.products$.subscribe(products => {
      if (products && products.products) {
        this.products = products.products;
        this.addProductStock();
        this.sortProducts();
        this.displayedProducts = [];
        this.currentPage = 1;
        this.loadMoreProducts();
      } else if (!products) {
        this.getProducts();
      }
    });

    this.sharedDataService.selectedCategory$.subscribe(category => {
      if (category) {
        this.listCategoryIds = category;
        this.getBrands();
      }
    });

    await this.getCategories();
    this.spinnerService.hideSpinner();
  }

  async getProducts() {
    this.spinnerService.showSpinner();
    let allProducts: any = await this.productService.read();
    console.log(allProducts);
    this.products = allProducts.products;
    this.addProductStock();
    this.sortProducts();
    this.loadMoreProducts();
    this.spinnerService.hideSpinner();
  }

  addProductStock() {
    if (!this.products) return;

    for (var i = 0; i < this.products.length; i++) {
      if (this.products[i].stock >= 10) {
        this.products[i].stockStatus = 'INSTOCK';
      } else if (this.products[i].stock != 0) {
        this.products[i].stockStatus = 'LOWSTOCK';
      } else {
        this.products[i].stockStatus = 'OUTOFSTOCK';
      }
    }
  }

  sortProducts() {
    if (!this.products) return;

    if (this.selectedSortOption === 'priceAsc') {
      this.products.sort((a, b) => a.price - b.price);
    } else if (this.selectedSortOption === 'priceDesc') {
      this.products.sort((a, b) => b.price - a.price);
    } else if (this.selectedSortOption === 'newest') {
      this.products.sort((a, b) => new Date(b.createdDate).getTime() - new Date(a.createdDate).getTime());
    }
    this.displayedProducts = [];
    this.currentPage = 1;
    this.loadMoreProducts();
  }

  loadMoreProducts() {
    if (this.loading || !this.products) return;
    this.loading = true;

    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = this.currentPage * this.itemsPerPage;
    const newProducts = this.products.slice(startIndex, endIndex);

    if (newProducts.length > 0) {
      this.displayedProducts = this.displayedProducts.concat(newProducts);
      this.currentPage++;
    }

    this.loading = false;
  }

  @HostListener('window:scroll', [])
  onScroll(): void {
    const scrollPosition = window.scrollY + window.innerHeight;
    const documentHeight = document.documentElement.scrollHeight;

    if (scrollPosition >= documentHeight - 10 && !this.loading) {
      this.loadMoreProducts();
    }
  }

  onImageError(event: any) {
    event.target.src = 'product.jpg';
  }

  async addToBasket(product: List_Product) {
    this.spinnerService.showSpinner();
    let basketItem: createBasketItem = new createBasketItem();
    basketItem.productId = product.id;
    basketItem.quantity = 1;
    await this.basketService.add(basketItem);
    this.customToastrService.message('Product added to basket', 'success', {
      messageType: ToastrMessageType.Success,
      position: ToastrPosition.TopRight
    });
    this.spinnerService.hideSpinner();
  }

  async getCategories() {
    this.spinnerService.showSpinner();
    let categoryList: listCategory[] = await this.categoryService.get();
    this.categories = this.convertToTreeNode(categoryList);
    this.spinnerService.hideSpinner();
  }

  convertToTreeNode(categories: listCategory[]): TreeNode[] {
    return categories.map(category => ({
      label: category.Name,
      data: category,
      children: category.subCategories ? this.convertToTreeNode(category.subCategories) : []
    }));
  }

  async onCategoryChange() {
    this.spinnerService.showSpinner();
    this.selectedNodes = [...this.selectedCategories];
    const selectedCategoryIds = this.getSelectedCategoriesFromTree(this.categories, []);

    if (selectedCategoryIds.length === 0) {
      await this.getProducts();
      return;
    }

    let categoryIdList: listCategoryId = new listCategoryId();
    categoryIdList.categoryIdList = selectedCategoryIds;
    const allProducts: any = await this.productService.getByCategory(categoryIdList);
    this.products = allProducts.products;
    this.addProductStock();

    this.displayedProducts = [];
    this.currentPage = 1;
    this.loadMoreProducts();
    this.listCategoryIds = new listCategoryId();
    this.listCategoryIds.categoryIdList = selectedCategoryIds;
    if (this.listCategoryIds != null) {
      await this.getBrands();
    }
    this.spinnerService.hideSpinner();
  }

  updateSelectedNodes() {
    this.selectedNodes = [...this.selectedCategories];
  }

  getSelectedCategoriesFromTree(nodes: TreeNode[] | null, selectedIds: string[]): string[] {
    if (!Array.isArray(nodes) || !this.selectedNodes) {
      return selectedIds;
    }

    this.selectedNodes.forEach(selectedNode => {
      if (selectedNode.data && selectedNode.data.Id && !selectedIds.includes(selectedNode.data.Id)) {
        selectedIds.push(selectedNode.data.Id);
      }
    });

    return selectedIds;
  }

  filterByPriceRange() {
    if (this.minPrice == null && this.maxPrice == null) {
      this.displayedProducts = [...this.products];
    } else {
      this.displayedProducts = this.products.filter(product =>
        (this.minPrice == null || product.price >= this.minPrice) &&
        (this.maxPrice == null || product.price <= this.maxPrice));
    }
  }

  onBrandChange() {
    this.filterByPriceRange();
    this.filterByBrand();
  }

  async getBrands() {
    if (!this.listCategoryIds || !this.listCategoryIds.categoryIdList || this.listCategoryIds.categoryIdList.length === 0) return;

    this.spinnerService.showSpinner();
    this.brandOptions = await this.brandService.getByCategories(this.listCategoryIds);
    this.spinnerService.hideSpinner();
  }

  filterByBrand() {
    if (this.selectedBrands == null || this.selectedBrands.length === 0) {
      this.displayedProducts = [...this.products];
    } else {
      this.displayedProducts = this.products.filter(product => {
        return this.selectedBrands.includes(product.brandName);
      });
    }
  }

  isProductLiked(productId: string): boolean {
    return this.likedProducts.has(productId);
  }

  async toggleLike(productId: string) {
    this.spinnerService.showSpinner();
    try {
      if (this.likedProducts.has(productId)) {
        this.likedProducts.delete(productId);
      } else {
        this.likedProducts.add(productId);
      }

      await this.productLikeService.create(productId);

      this.customToastrService.message(
        this.isProductLiked(productId) ? 'Product added to favorites' : 'Product removed from favorites',
        'success',
        {
          messageType: ToastrMessageType.Success,
          position: ToastrPosition.TopRight
        }
      );
    } catch (error) {
      this.likedProducts.has(productId)
        ? this.likedProducts.delete(productId)
        : this.likedProducts.add(productId);
      console.error('Error toggling product like:', error);
    }
    this.spinnerService.hideSpinner();
  }

  navigateToProduct(productId: string) {
    this.spinnerService.showSpinner();
    this.router.navigate([`/products/productdetail/${productId}`]).then(() => {
      window.scrollTo({ top: 0, behavior: 'smooth' });
    });
  }
}

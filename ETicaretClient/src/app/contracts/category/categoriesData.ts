export class Categories {
  name: string;
  parentCategoryId: string | null;
  subCategoriesId: string;
  id: string;
  createdDate: string;
  updatedDate: string;
}

export class CategoriesData {
  categories: Categories[];
}

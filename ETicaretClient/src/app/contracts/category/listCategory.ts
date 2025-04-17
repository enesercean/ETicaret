export class listCategory {
  Id: string;
  Name: string;
  ParentCategoryId: string | null;
  selected?: boolean; // Checkbox seçili mi?
  subCategories?: listCategory[]; // Alt kategoriler
}

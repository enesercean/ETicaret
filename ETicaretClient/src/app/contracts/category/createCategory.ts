export class CreateCategory {
  name: string;
  parentCategoryId?: string;
  constructor(name: string, parentCategoryId?: string) {
    this.name = name;
    this.parentCategoryId = parentCategoryId;
  }
}

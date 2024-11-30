import { Routes } from '@angular/router';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductCreateComponent } from './components/product-create/product-create.component';

export const appRoutes: Routes = [
    {path:'', redirectTo: '/products', pathMatch: 'full'},
    {path:'products', component: ProductListComponent},
    {path: 'products/create', component : ProductCreateComponent}
];
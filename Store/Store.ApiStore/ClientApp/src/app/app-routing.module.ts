import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';


const routes: Routes = [
    {
        path: 'products',
        loadChildren: './product/product.module#ProductModule'
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, {
        // preload all modules; optionally we could
        // implement a custom preloading strategy for just some
        // of the modules (PRs welcome 😉)
        preloadingStrategy: PreloadAllModules
    })],
    exports: [RouterModule]
})
export class AppRoutingModule { }

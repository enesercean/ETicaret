import { RenderMode, ServerRoute } from '@angular/ssr';

export const serverRoutes: ServerRoute[] = [
  // Tüm products rotaları için client rendering
  {
    path: 'products/**',
    renderMode: RenderMode.Client
  },
  // Tüm account rotaları için client rendering
  {
    path: 'account/**',
    renderMode: RenderMode.Client
  },
  // Diğer tüm parametreli rotalar
  {
    path: '**/*/:*',  // İç içe rotalar ve parametreler
    renderMode: RenderMode.Client
  },
  {
    path: '*/:*',     // Temel parametreli rotalar
    renderMode: RenderMode.Client
  },
  // Kalan rotalar için prerendering
  {
    path: '**',
    renderMode: RenderMode.Prerender
  }
];

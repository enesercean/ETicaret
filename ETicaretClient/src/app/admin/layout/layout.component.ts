import { Component, OnInit, ViewChild } from '@angular/core';
import { AlertifyService, MessageType, Position } from '../../services/admin/alertify.service';
import { MenuItem } from 'primeng/api';
import { MatSidenav } from '@angular/material/sidenav';

@Component({
    selector: 'app-layout',
    templateUrl: './layout.component.html',
    styleUrls: ['./layout.component.scss'],
    standalone: false
})
export class LayoutComponent implements OnInit {
  @ViewChild('sidenav') sidenav!: MatSidenav;

  sidebarCollapsed = false;
  breadcrumbItems: MenuItem[] = [];
  home: MenuItem = { icon: 'pi pi-home', routerLink: '/admin' };

  constructor() { }

  ngOnInit(): void {
    this.breadcrumbItems = [
      { label: 'Admin' },
      { label: 'Dashboard' }
    ];
  }

  toggleSidebar(): void {
    this.sidebarCollapsed = !this.sidebarCollapsed;
  }



}

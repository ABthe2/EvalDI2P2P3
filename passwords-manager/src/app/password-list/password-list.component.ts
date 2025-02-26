import {Component, OnInit} from '@angular/core';
import {ApiService} from "../../services/api.service";
import {MatList, MatListItem} from "@angular/material/list";
import {MatIcon} from "@angular/material/icon";
import {MatLine} from "@angular/material/core";
import {MatButton, MatIconButton} from "@angular/material/button";
import {CommonModule} from "@angular/common";
import {
  MatCell,
  MatCellDef, MatColumnDef,
  MatHeaderCell,
  MatHeaderCellDef,
  MatHeaderRow, MatHeaderRowDef,
  MatRow, MatRowDef,
  MatTable
} from "@angular/material/table";

@Component({
  selector: 'app-password-list',
  standalone: true,
  imports: [
    CommonModule,
    MatList,
    MatListItem,
    MatIcon,
    MatLine,
    MatIconButton,
    MatTable,
    MatHeaderRow,
    MatRow,
    MatCell,
    MatHeaderCell,
    MatButton,
    MatCellDef,
    MatHeaderCellDef,
    MatColumnDef,
    MatHeaderRowDef,
    MatRowDef
  ],
  templateUrl: './password-list.component.html',
  styleUrl: './password-list.component.css'
})
export class PasswordListComponent implements OnInit {
  passwords: any = []
  displayedColumns: string[] = ['application', 'name', 'password', 'actions']
  title = "Liste des mots de passe"

  constructor(private apiService: ApiService) {}

  ngOnInit() {
    this.fetchPasswords()
  }

  fetchPasswords() {
    this.apiService.getPasswords().subscribe({
      next: (accounts) => {
        this.passwords = accounts
      },
      error: (error) => {
        console.error(error)
      }
    })
  }

  deletePassword(accountId: number) {
    this.apiService.deleteAccount(accountId).subscribe({
      next: () => {
        this.fetchPasswords()
      },
      error: (error) => {
        console.error(error)
      }
    })
  }
}

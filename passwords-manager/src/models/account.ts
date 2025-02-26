import {Application} from "./application";

export class Account {
  idAccount: number;
  name: string;
  password: string;
  idApplication: number;
  application: Application;

  constructor() {
    this.idAccount = 0;
    this.name = '';
    this.password = '';
    this.idApplication = 0;
    this.application = new Application();
  }
}

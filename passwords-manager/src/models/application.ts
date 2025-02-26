import {ApplicationType} from "./application-type";

export class Application {
  idApplication: number;
  name: string;
  idApplicationType: number;
  applicationType: ApplicationType;

  constructor() {
    this.idApplication = 0;
    this.name = '';
    this.idApplicationType = 0;
    this.applicationType = new ApplicationType();
  }
}

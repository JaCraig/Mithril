import { IRouter } from '../Router/Interfaces/IRouter'

// Routing configuration interface
export interface IRouteConfiguration {
    // called when configuring the routes
    configureRouting(router: IRouter): void;
}
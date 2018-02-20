import { Component } from 'vue';
import { ComponentConfiguration } from '../../../..//ezs-common/src/model/client/component-configuration.model';
import { Provider } from '../../../../ezs-common/src/provider';

export const COMPONENT_GLOBAL_CONSTANT: Array < ComponentConfiguration > = Array.prototype.concat(Provider.retrieveComponents());
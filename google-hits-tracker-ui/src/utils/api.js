import _ from 'lodash';
import axios from 'axios';

export default {
  async ajax(method, url, data, config) {
    try {
      let response = null;
      if (method === 'get') response = await axios.get(url, config);
      if (method === 'post') response = await axios.post(url, data, config);
      if (method === 'put') response = await axios.put(url, data, config);
      if (method === 'delete') response = await axios.delete(url, data, config);

      return response.data;
    } catch (err) {
      if (axios.isCancel(err)) throw err;

      if (err && err.response) {
        const error = new Error(err.message);
        error.data = err.response.data;

        const isManagingStatusCodesManually = _.has(error.data, 'success') && _.has(error.data, 'result');
        if (isManagingStatusCodesManually) error.data = error.data.result;
        if (!_.isObject(error.data)) error.data = null;

        error.status = err.response.status;

        throw error;
      }

      if (err) {
        const error = new Error(err.message);
        error.data = null;
        error.status = err.request ?.status || 0;

        throw error;
      }

      const error = new Error('An error has occurred');
      error.data = null;
      error.status = 0;

      throw error;
    }
  },

  get(url, config) {
    return this.ajax('get', url, null, config);
  },

  post(url, data, config) {
    return this.ajax('post', url, data, config);
  },

  put(url, data, config) {
    return this.ajax('put', url, data, config);
  },

  delete(url, data, config) {
    return this.ajax('delete', url, data, config);
  },

  createCancelToken: () => axios.CancelToken.source(),

  isCancel: (err) => axios.isCancel(err),
};

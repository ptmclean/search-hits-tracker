
import React, { useState, useRef } from 'react';
import api from 'utils/api';
import SearchResults from './SearchResults';
import styles from './Home.css';

const Home = () => {
  const [urlToTest, setUrlToTest] = useState('');
  const [searchTerm, setSearchTerm] = useState('');
  const [formErrors, setFormErrors] = useState('');
  const buttonRef = useRef(null);
  const [searchResults, setSearchResults] = useState('');

  const getSearchRankings = async (e) => {
    e.preventDefault();
    buttonRef.current.disabled = true;
    setFormErrors(null);
    if (!searchTerm || !urlToTest) {
      setFormErrors('Please provide both a url to test and a search term.');
      buttonRef.current.disabled = false;
      return;
    }
    try {
      setSearchResults(await api.get(`http://localhost:5000/api/searchHits?searchTerm=${searchTerm}&testUrl=${urlToTest}`));
    } catch (err) {
      // eslint-disable-next-line no-console
      console.log(err);
      setFormErrors('An Error occured trying to get your results. Please try again.');
      buttonRef.current.disabled = false;
    }
    buttonRef.current.disabled = false;
  };

  return (
    <div className={styles.homeContent}>
      Check how we are tracking with our search ranking by entering a search term and the URL that you want to rank. The tracker will let you know all the entries in the top 100 search results that contain the URL to test.
      <form className={styles.trackerForm}>
        <label htmlFor="urlToTest">
          <span className={styles.formLabel}>Site to Test</span>
          <input name="urlToTest" type="text" value={urlToTest} placeholder="www.sympli.com.au" onChange={(e) => setUrlToTest(e.target.value)} required />
        </label>
        <label htmlFor="searchTerm">
          <span className={styles.formLabel}>Search Term</span>
          <input name="searchTerm" type="text" value={searchTerm} placeholder="e-settlement" onChange={(e) => setSearchTerm(e.target.value)} required />
        </label>
        <button type="button" ref={buttonRef} onClick={(e) => getSearchRankings(e)}>Check Rankings</button>
        <div className={styles.error}>{formErrors}</div>
      </form>
      {searchResults ? searchResults.map((sr) => (
        <React.Fragment key={sr.searchEngine}>
          <h2>{`${sr.searchEngine} Search Hits`}</h2>
          <SearchResults searchResults={sr} />
        </React.Fragment>
      )) : null}
    </div>
  );
};

export default Home;

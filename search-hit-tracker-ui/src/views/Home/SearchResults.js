import React from 'react';
import PropTypes from 'prop-types';
import styles from './SearchResults.css';

const SearchResults = ({ searchResults }) => {
  if (!searchResults || !searchResults.matchingResults.length > 0) {
    return (
      <div className={styles.searchResultsContainer}>
        No Search results found!
      </div>
    );
  }
  return (
    <div className={styles.searchResultsContainer}>
      <h3 className={styles.rankingHeader}>Ranking</h3>
      <h3 className={styles.titleHeader}>Title</h3>
      {searchResults.matchingResults.map((s) => (
        <div key={s.ranking} className={styles.searchResult}>
          <div className={styles.ranking}>{s.ranking}</div>
          <a className={styles.title} href={s.address} target="_blank" rel="noopener noreferrer">{s.title}</a>
        </div>
      ))}
    </div>
  );
};

SearchResults.propTypes = {
  searchResults: PropTypes.oneOfType([PropTypes.string, PropTypes.object]).isRequired,
};

export default SearchResults;
